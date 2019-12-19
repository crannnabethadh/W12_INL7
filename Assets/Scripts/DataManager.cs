using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using W12_INLAPI;

public class DataManager : MonoBehaviour
{
    [SerializeField] Dropdown countriesDropdown;
    [SerializeField] InputField filterInputField;
    [SerializeField] Text citiesText;
    [SerializeField] Text citiesHeadingText;
    [SerializeField] Text numberOfCitiesFound;
    [SerializeField] Button nextPageButton;
    [SerializeField] Button prevPageButton;

    List<Country> countries = new List<Country>();
    List<City> cities = new List<City>();
    const int citiesPerPage = 10;
    int numberOfPages;
    int numberOfCitiesTotal;
    int actualPage;
    int countryId;

    private void Start()
    {
        InitializeDBConnection();
        prevPageButton.interactable = false;
        nextPageButton.interactable = false;
        LoadCountries();
    }

    private void InitializeDBConnection()
    {
        DBConnectionParameters.Server = "127.0.0.1";
        DBConnectionParameters.Port = "3306";
        DBConnectionParameters.Database = "sakila";
        DBConnectionParameters.Uid = "client";
        DBConnectionParameters.Pwd = "$3cr3t3t";
    }

    public void LoadCountries()
    {
        countries = DataAccess.GetCountries(filterInputField.text);
        countriesDropdown.ClearOptions();
        foreach (Country c in countries)
        {
            countriesDropdown.options.Add(new Dropdown.OptionData(c.Name));
        }
        countriesDropdown.captionText.text = countriesDropdown.options[0].text;
        LoadCities();
    }

    public void LoadCities()
    {
        //Find country id which corresponds to the country name selected in dropdown

        //LINQ. We haven't seen it yet
        //countryId = countries.Where(x => x.Name == countriesDropdown.captionText.text).FirstOrDefault().Id;

        //A sequential search over countries list
        foreach(Country c in countries)
        {
            if (c.Name == countriesDropdown.captionText.text)
            {
                countryId = c.Id;
            }
        }

        actualPage = 0;
        numberOfCitiesTotal = DataAccess.GetCitiesCount(countryId);
        numberOfPages = numberOfCitiesTotal / citiesPerPage + 1;
        
        citiesText.text = string.Empty;
        citiesHeadingText.text = "Cities from " + countriesDropdown.captionText.text;
        numberOfCitiesFound.text = $"{numberOfCitiesTotal} cities found";

        nextPageButton.interactable = false;
        if (numberOfPages > 1)
        {
            nextPageButton.interactable = true;
        }

        ShowActualPage();

    }

    private void ShowActualPage()
    {
        citiesText.text = string.Empty;
        cities = DataAccess.GetCities(countryId, actualPage * citiesPerPage, citiesPerPage);
        for (int i = 0; i < cities.Count; i++)
        {
            citiesText.text += (actualPage * citiesPerPage + i + 1) + ". " + cities[i].Name + "\n";
        }
    }

    public void NextPage()
    {
        actualPage++;
        ShowActualPage();
        if (actualPage == numberOfPages - 1)
        {
            nextPageButton.interactable = false;
        }
        if (actualPage > 0)
        {
            prevPageButton.interactable = true;
        }
    }

    public void PrevPage()
    {
        actualPage--;
        ShowActualPage();
        if (actualPage <= 0)
        {
            prevPageButton.interactable = false;
        }
        if (actualPage <= numberOfPages)
        {
            nextPageButton.interactable = true;
        }
    }


}
