import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import FavoriteScreen from "./FavoriteScreen";
import RecipesStack from "./RecipesStack";
import { useFavorites } from "../context/FavoritesContext";
import { Ionicons } from "@expo/vector-icons";
import { getFocusedRouteNameFromRoute } from "@react-navigation/native";

const Tab = createBottomTabNavigator();

const Tabs = () => {
  const { favorites } = useFavorites();

  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        headerShown: false,
        tabBarActiveTintColor: "#1e6f8e",
        tabBarInactiveTintColor: "#94a3b8",
        tabBarIcon: ({ color, size, focused }) => (
          <Ionicons
            name={
              route.name === "Recipes"
                ? "restaurant"
                : focused
                ? "heart"
                : "heart-outline"
            }
            size={size}
            color={color}
          />
        ),
      })}
    >
      <Tab.Screen
        name="Recipes"
        component={RecipesStack}
        options={({ route }) => ({
          headerShown: false,
          title: "Рецепты",
          tabBarStyle: {
            display:
              getFocusedRouteNameFromRoute(route) === "RecipeDetail"
                ? "none"
                : "flex",
          },
        })}
      />

      <Tab.Screen
        name="Favorites"
        component={FavoriteScreen}
        options={{
          headerShown: true,
          title: "Избранное",
          tabBarBadge: favorites.length || undefined,
        }}
      />
    </Tab.Navigator>
  );
};

export default Tabs;