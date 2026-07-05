
import { Pressable, Text, View } from "react-native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

import RecipeListScreen from "../screens/RecipeListScreen";
import RecipeDetailScreen from "../screens/RecipeDetailScreen";
import CategoryListScreen from "../screens/CategoryListScreen";
import AboutScreen from "../screens/AboutScreen";
import { RECIPES } from "../data/recipes";


const Stack = createNativeStackNavigator();

const RecipesStack = () =>{
    return (

          <Stack.Navigator
            initialRouteName="RecipeList"
            screenOptions={{
              headerStyle: { backgroundColor: "#20232A" },
              headerTintColor: "#61DAFB",
              headerTitleStyle: { fontWeight: "bold" },
            }}
          >
            <Stack.Screen
              name="RecipeList"
              component={RecipeListScreen}
              options={({ navigation }) => ({
                title: "Что приготовить",
                headerRight: () => {
                  const r = RECIPES[Math.floor(Math.random() * RECIPES.length)];
                  return (
                    <View style={{ flexDirection: "row", gap: 16, alignItems: "center", marginRight: 10 }}>
                      <Pressable
                        onPress={() => navigation.navigate("About")}
                      >
                        <Text style={{ fontSize: 24 }}>ℹ️</Text>
                      </Pressable>
                      <Pressable
                        onPress={() => navigation.navigate("RecipeDetail", { recipe: r })}
                      >
                        <Text style={{ fontSize: 24 }}>🎲</Text>
                      </Pressable>
                    </View>
                  );
                },
              })}
            />
            <Stack.Screen
              name="RecipeDetail"
              component={RecipeDetailScreen}
              options={({ route, navigation }) => ({
                title: route.params.recipe.name,
                headerRight: () => {
                  const r = RECIPES[Math.floor(Math.random() * RECIPES.length)];
    
                  return (
                    <Pressable
                      onPress={() => navigation.push("RecipeDetail", { recipe: r })}
                    >
                      <Text>Случайный рецепт</Text>
                    </Pressable>
                  );
                },
              })}
            />
            <Stack.Screen
              name="CategoryList"
              component={CategoryListScreen}
              options={({ route }) => ({
                title: `Категория: ${route.params.category}`,
              })}
            />
            <Stack.Screen
              name="About"
              component={AboutScreen}
              options={{ title: "О приложении" }}
            />
          </Stack.Navigator>
      );
}

export default RecipesStack