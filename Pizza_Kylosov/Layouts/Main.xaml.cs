﻿using Pizza_Kylosov.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using static Pizza_Kylosov.Class.Dish;

namespace Pizza_Kylosov.Layouts
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public MainWindow mainWindow;
        public List<Dish> dishs = new List<Dish>();
        Grid CreateGrid(int i)
        {
            var bc = new BrushConverter();
            Grid global = new Grid();
            global.Height = 100;
            global.Background = (Brush)bc.ConvertFrom("#222222");
            if (i > 0) global.Margin = new Thickness(0, 10, 0, 0);
            return global;
        }

        Image CreateLogo(int i)
        {
            Image logo = new Image();
            if (File.Exists(mainWindow.localPath + @"\image\dish\" + dishs[i].img + ".png"))
                logo.Source = new BitmapImage(new Uri(mainWindow.localPath + @"\image\dish\" + dishs[i].img + ".png"));
            else
                logo.Source = new BitmapImage(new Uri(mainWindow.localPath + @"\image\icon.png"));

            logo.HorizontalAlignment = HorizontalAlignment.Left;
            logo.Height = 50;
            logo.Margin = new Thickness(10, 10, 0, -10);
            logo.VerticalAlignment = VerticalAlignment.Top;
            logo.Width = 50;
            return logo;
        }

        Label CreateLabel(string str, HorizontalAlignment ha, VerticalAlignment va , double[] margin, FontWeight fw)
        {
            Label name = new Label();
            name.Content = str;
            name.HorizontalAlignment = ha;
            name.VerticalAlignment = va;
            name.Margin = new Thickness(margin[0], margin[1], margin[2], margin[3]);
            name.FontWeight = fw;
            name.Foreground = Brushes.White;
            return name;
        }

        Button CreateButton(string str, HorizontalAlignment ha, VerticalAlignment va, double[] margin, int width, int i)
        {
            Button button = new Button();

            button.Content = str;
            button.HorizontalAlignment = ha;
            button.VerticalAlignment = va;
            button.Margin = new Thickness(margin[0], margin[1], margin[2], margin[3]);
            button.Width = width;
            button.Tag = i;
            return button;
        }
        
        Label AddIng(int i)
        {
            if (dishs[i].ingredients.Count != 0) 
            {
                Label ingredient = new Label();
                string str_ingredient = "";
                for (int j = 0; j < dishs[i].ingredients.Count; j++) 
                {
                    str_ingredient += dishs[i].ingredients[j].name;
                    if (j != dishs[i].ingredients.Count - 1)
                    {
                        str_ingredient += ", ";
                    }
                }

                ingredient = CreateLabel("Состав: " + str_ingredient, HorizontalAlignment.Left, VerticalAlignment.Top, new[] { 65.0, 40, 0, 0 }, FontWeights.Normal);
                return ingredient;
            }
            return null;
        }
        
        public void CreatePizza()
        {
            for (int i = 0; i < dishs.Count; i++)
            {
                var bc = new BrushConverter();

                Grid global;
                Image logo;

                Label name;
                Label description;
                Label ingr;
                Label price;
                Label wes; 

                Button button1 = new Button();
                Button button2 = new Button();
                Button button3 = new Button();
                Button minus = new Button();
                Button plus = new Button();

                TextBox count = new TextBox();
                CheckBox order = new CheckBox();

                global = CreateGrid(i);

                logo = CreateLogo(i);

                name = CreateLabel(dishs[i].name, HorizontalAlignment.Left, VerticalAlignment.Top, new[] { 65.0, 0, 0, 0 }, FontWeights.Bold);

                description = CreateLabel(dishs[i].description, HorizontalAlignment.Left, VerticalAlignment.Top, new[] { 65.0, 20.0, 0, 0 }, FontWeights.Normal);

                ingr = AddIng(i);

                price = CreateLabel("Цена: " + dishs[i].sizes[0].price + " р.", HorizontalAlignment.Left, VerticalAlignment.Bottom, new[] { 65.0, 0, 0, 10 }, FontWeights.Normal);
                
                wes = CreateLabel("Вес: " + dishs[i].sizes[0].wes + " гр.", HorizontalAlignment.Left, VerticalAlignment.Bottom, new[] { 236.0, 0, 0, 10 }, FontWeights.Normal);

                button1 = CreateButton(dishs[i].sizes[0].size + " см.", HorizontalAlignment.Right, VerticalAlignment.Top, new[] { 0.0, 10, 110, 0 }, 45, i);
                button1.Background = (Brush)bc.ConvertFrom("#00ba78");
                button1.Foreground = Brushes.White;
                button1.Click += delegate
                {
                    price.Content = "Цена: " + dishs[int.Parse(button1.Tag.ToString())].sizes[0].price + " р.";
                    wes.Content = "Вес: " + dishs[int.Parse(button1.Tag.ToString())].sizes[0].wes + " гр.";
                    button1.Background = (Brush)bc.ConvertFrom("#00ba78");
                    button1.Foreground = Brushes.White;


                    button2.Background = (Brush)bc.ConvertFrom("#228e5d");
                    button2.Foreground = Brushes.White;
                    button3.Background = (Brush)bc.ConvertFrom("#228e5d");
                    button3.Foreground = Brushes.White;

                    dishs[int.Parse(button1.Tag.ToString())].activeSize = 0;
                    count.Text = dishs[int.Parse(button1.Tag.ToString())].sizes[0].countOrder.ToString();
                    order.IsChecked = dishs[int.Parse(button1.Tag.ToString())].sizes[0].orders;
                };

                button2 = CreateButton(dishs[i].sizes[1].size + " см.", HorizontalAlignment.Right, VerticalAlignment.Top, new[] { 0.0, 10, 60, 0 },45, i);
                button2.Click += delegate
                {
                    price.Content = "Цена: " + dishs[int.Parse(button2.Tag.ToString())].sizes[1].price + " р."; //Обновляем цену
                    wes.Content = "Вес: " + dishs[int.Parse(button2.Tag.ToString())].sizes[1].wes + " гр.";
                    button2.Background = (Brush)bc.ConvertFrom("#00ba78");
                    button2.Foreground = Brushes.White;


                    button1.Background = (Brush)bc.ConvertFrom("#228e5d");
                    button1.Foreground = Brushes.White;
                    button3.Background = (Brush)bc.ConvertFrom("#228e5d");
                    button3.Foreground = Brushes.White;


                    dishs[int.Parse(button1.Tag.ToString())].activeSize = 1;
                    count.Text = dishs[int.Parse(button1.Tag.ToString())].sizes[1].countOrder.ToString();
                    order.IsChecked = dishs[int.Parse(button1.Tag.ToString())].sizes[1].orders;
                };

                button3 = CreateButton(dishs[i].sizes[2].size + " см.", HorizontalAlignment.Right, VerticalAlignment.Top, new[] { 0.0, 10, 10, 0 }, 45, i); 
                button3.Click += delegate 
                {
                    price.Content = "Цена: " + dishs[int.Parse(button3.Tag.ToString())].sizes[2].price + " р."; //Обновляем цену
                    wes.Content = "Вес: " + dishs[int.Parse(button3.Tag.ToString())].sizes[2].wes + " гр.";
                    button3.Background = (Brush)bc.ConvertFrom("#00ba78");
                    button3.Foreground = (Brush)bc.ConvertFrom("#228e5d");


                    button1.Background = (Brush)bc.ConvertFrom("#228e5d");
                    button1.Foreground = Brushes.White;
                    button2.Background = (Brush)bc.ConvertFrom("#228e5d");
                    button2.Foreground = Brushes.White;


                    dishs[int.Parse(button1.Tag.ToString())].activeSize = 2;
                    count.Text = dishs[int.Parse(button1.Tag.ToString())].sizes[2].countOrder.ToString();
                    order.IsChecked = dishs[int.Parse(button1.Tag.ToString())].sizes[2].orders;
                };

                minus = CreateButton("-", HorizontalAlignment.Right, VerticalAlignment.Bottom, new[] { 0.0, 0, 103.6f, 10 }, 19, i);
                minus.Click += delegate 
                {
                    if (count.Text != "")
                    {
                        if (int.Parse(count.Text) > 0)
                        {
                            count.Text = (int.Parse(count.Text) - 1).ToString();

                            int id = int.Parse(minus.Tag.ToString());
                            dishs[id].sizes[dishs[id].activeSize].countOrder = int.Parse(count.Text);
                            if (dishs[id].sizes[dishs[id].activeSize].countOrder != 0) 
                                price.Content = "Цена: " + dishs[int.Parse(button1.Tag.ToString())].sizes[0].price / dishs[id].sizes[dishs[id].activeSize].countOrder + " р.";
                        }
                    }
                };

                plus = CreateButton("+", HorizontalAlignment.Right, VerticalAlignment.Bottom, new[] {0.0, 0, 9.6f, 10}, 19, i);
                plus.Click += delegate 
                {
                    if (count.Text != "")
                    {
                        if (int.Parse(count.Text) < 15)
                        {
                            count.Text = (int.Parse(count.Text) + 1).ToString();

                            int id = int.Parse(plus.Tag.ToString());
                            dishs[id].sizes[dishs[id].activeSize].countOrder = int.Parse(count.Text);
                            price.Content = "Цена: " + dishs[int.Parse(button1.Tag.ToString())].sizes[0].price * dishs[id].sizes[dishs[id].activeSize].countOrder + " р.";
                        }
                    }
                };

                count.Text = "0";
                count.HorizontalAlignment = HorizontalAlignment.Right;
                count.VerticalAlignment = VerticalAlignment.Bottom;
                count.Margin = new Thickness(0, 0, 33.6f, 10);
                count.TextWrapping = TextWrapping.Wrap;
                count.HorizontalContentAlignment = HorizontalAlignment.Center;
                count.Width = 65;
                count.Height = 19;
                count.Tag = i; 

                order.Content = "Выбрать";
                order.HorizontalAlignment = HorizontalAlignment.Right;
                order.VerticalAlignment = VerticalAlignment.Bottom;
                order.Margin = new Thickness(0, 0, 128, 13);
                order.Foreground = Brushes.White;
                order.Tag = i;
                order.Click += delegate
                {
                    int id = int.Parse(order.Tag.ToString());
                    dishs[id].sizes[dishs[id].activeSize].orders = (bool)order.IsChecked;
                    int x = int.Parse((OrderCount.Content.ToString().Remove(0, 10)).Replace(")", ""));
                    OrderCount.Content = $"Заказать ({ ((bool)order.IsChecked ? $"{++x}" : $"{--x}")})";
                };

                global.Children.Add(logo);
                global.Children.Add(name);
                global.Children.Add(description);
                global.Children.Add(ingr);
                global.Children.Add(price);
                global.Children.Add(wes);
                global.Children.Add(button1);
                global.Children.Add(button2);
                global.Children.Add(button3);
                global.Children.Add(minus);
                global.Children.Add(count);
                global.Children.Add(plus);
                global.Children.Add(order);

                parent.Children.Add(global);
            }
        }

        public Main(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            //Пицца 1
            Dish newDish = new Dish();
            newDish.img = "img-1";
            newDish.name = "Сливочная";
            newDish.description = "Пицца - итальянское национальное блюдо в виде круглой открытой дрожжевой лепёшки";


            Dish.Ingredient newIngredient = new Dish.Ingredient();
            newIngredient.name = "соус «Кунжутный»";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient(); ;
            newIngredient.name = "сыр «Моцарелла»";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient(); ;
            newIngredient.name = "сыр «Моцарелла мягкий»";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient(); ;
            newIngredient.name = "помидоры";
            newDish.ingredients.Add(newIngredient);


            Dish.Sizes newSize = new Dish.Sizes();
            newSize.size = 23;
            newSize.price = 380;
            newSize.wes = 530;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 30;
            newSize.price = 760;
            newSize.wes = 560;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 40;
            newSize.price = 1210;
            newSize.wes = 730;
            newDish.sizes.Add(newSize);

            dishs.Add(newDish);

            // Pizza 2
            newDish = new Dish();
            newDish.img = "img-3";
            newDish.name = "Пепперони";
            newDish.description = "Пицца с пикантной пепперони, моцареллой и томатным соусом.";

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "пепперони";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "сыр «Моцарелла»";
            newDish.ingredients.Add(newIngredient);

            newSize = new Dish.Sizes();
            newSize.size = 23;
            newSize.price = 400;
            newSize.wes = 550;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 30;
            newSize.price = 800;
            newSize.wes = 600;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 40;
            newSize.price = 1200;
            newSize.wes = 750;
            newDish.sizes.Add(newSize);

            dishs.Add(newDish);

            // Pizza 3
            newDish = new Dish();
            newDish.img = "img-4";
            newDish.name = "Гавайская";
            newDish.description = "Пицца с ветчиной и ананасами.";

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "ветчина";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "ананасы";
            newDish.ingredients.Add(newIngredient);

            newSize = new Dish.Sizes();
            newSize.size = 23;
            newSize.price = 450;
            newSize.wes = 600;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 30;
            newSize.price = 900;
            newSize.wes = 650;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 40;
            newSize.price = 1350;
            newSize.wes = 800;
            newDish.sizes.Add(newSize);

            dishs.Add(newDish);

            // Pizza 4
            newDish = new Dish();
            newDish.img = "img-5";
            newDish.name = "Вегетарианская";
            newDish.description = "Пицца с миксом овощей.";

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "болгарский перец";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "томаты";
            newDish.ingredients.Add(newIngredient);

            newIngredient = new Dish.Ingredient();
            newIngredient.name = "шампиньоны";
            newDish.ingredients.Add(newIngredient);

            newSize = new Dish.Sizes();
            newSize.size = 23;
            newSize.price = 400;
            newSize.wes = 550;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 30;
            newSize.price = 800;
            newSize.wes = 600;
            newDish.sizes.Add(newSize);

            newSize = new Dish.Sizes();
            newSize.size = 40;
            newSize.price = 1200;
            newSize.wes = 750;
            newDish.sizes.Add(newSize);

            dishs.Add(newDish);

            CreatePizza();
        }
    }
}
