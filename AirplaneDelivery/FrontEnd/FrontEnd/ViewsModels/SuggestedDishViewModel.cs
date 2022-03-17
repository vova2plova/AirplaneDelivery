using Acr.UserDialogs;
using DAL.Models;
using FrontEnd.OnlineServices;
using FrontEnd.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FrontEnd.ViewsModels
{
    internal class SuggestedDishViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public async void LoadRecipes(StackLayout List)
        {
            var recipes = await MainService.RecipeService.GetRecipesByIdProducts(Preferences.Get("current_product", 0));
            if (recipes.IsSuccessStatusCode)
            {
                Recipes = new ObservableCollection<Recipe>(recipes.Content);
                BindableLayout.SetItemsSource(List, Recipes);
            }
        }

        public Command<Recipe> SelectRecipeCommand => new Command<Recipe>(async Recipe =>
        {
            using (UserDialogs.Instance.Loading("Страница загружается", null, null, true, MaskType.Black))
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DetailDishPage(Recipe));
            }
        });
    }
}
