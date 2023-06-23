﻿
var map, searchManager;

function GetMap() {
    map = new Microsoft.Maps.Map('#myMap', {});

    //Load the Autosuggest and search modules.
    Microsoft.Maps.loadModule(['Microsoft.Maps.AutoSuggest', 'Microsoft.Maps.Search'], function () {
        //Create instances of the Autosuggest and Search managers.		

        var autosuggestManager = new Microsoft.Maps.AutosuggestManager({ map: map });
        searchManager = new Microsoft.Maps.Search.SearchManager(map);

        //Create a jQuery autocomplete UI widget.
        $("#FormattedAddress").autocomplete({
            minLength: 3,   //Don't ask for suggestions until atleast 3 characters have been typed.
            source: function (request, response) {
                //Get suggestions from the AutosuggestManager.
                autosuggestManager.getSuggestions(request.term, response);
            },
            select: function (event, ui) {
                //When a suggestion has been selected.

                //Check to see if the suggestion has a location value.
                if (ui.item.location) {
                    suggestionSelected(ui.item);
                } else {
                    //If it doesn't, we need to geocode it.
                    geocodeSuggestion(ui.item);
                }
            },
        }).autocomplete("instance")._renderItem = function (ul, item) {
            //Format the displayed suggestion to show the formatted suggestion string.
            return $("<li>")
                .append("<a>" + item.formattedSuggestion + "</a>")
                .appendTo(ul);
            };

        if ($("#FormattedAddress").val() != '') {
            geocodeInitialAddress($("#FormattedAddress").val());
        }

    });
}

function geocodeSuggestion(suggestion) {
    var searchRequest = {
        where: suggestion.formattedSuggestion,
        callback: function (r) {
            //Add the first result to the map and zoom into it.
            if (r && r.results && r.results.length > 0) {
                //Enrich the suggestion with a location and best view information.
                suggestion.location = r.results[0].location;
                suggestion.bestView = r.results[0].bestView;

                //Return the enriched suggestion.
                suggestionSelected(suggestion);
            }
        },
        errorCallback: function (e) {
            //If there is an error, alert the user about it.
            alert("No results found.");
        }
    };

    //Make the geocode request.
    searchManager.geocode(searchRequest);
}

function geocodeInitialAddress(address) {
    var requestOptions = {
        where: address,
        callback: function (answer) {
            map.entities.clear();
            //Show the suggestion as a pushpin and center map over it.
            var pin = new Microsoft.Maps.Pushpin(answer.results[0].location);
            map.entities.push(pin);
            map.setView({ bounds: answer.results[0].bestView });
        }
    };
    searchManager.geocode(requestOptions);
}

function suggestionSelected(suggestion) {

    document.getElementById('addressLineTbx').value = suggestion.address.addressLine || '';
    document.getElementById('cityTbx').value = suggestion.address.locality || '';
    document.getElementById('stateTbx').value = suggestion.address.adminDistrict || '';
    document.getElementById('postalCodeTbx').value = suggestion.address.postalCode || '';

    //Remove previously selected suggestions from the map.
    map.entities.clear();

    //Show the suggestion as a pushpin and center map over it.
    var pin = new Microsoft.Maps.Pushpin(suggestion.location);
    map.entities.push(pin);
    document.getElementById('FormattedAddress').value = suggestion.address.formattedAddress || '';


    //Set the map view to the best view for the result, if defined.
    if (suggestion.bestView) {
        map.setView({ bounds: suggestion.bestView });
    } else {
        //If best map view is not known, approximate the zoom level based on the type of entity the result is.
        var zoom = 12;

        switch (suggestion.entitySubType) {
            case 'CountryRegion':
                zoom = 3;
                break;
            case 'AdminDivision1':
                zoom = 6;
                break;
            case 'AdminDivision2':
                zoom = 10;
                break;
            default:
                break;
        }

        map.setView({ center: suggestion.location, zoom: zoom });

    }
}