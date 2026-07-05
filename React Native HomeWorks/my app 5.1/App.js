import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import FavoritesProvider from "./context/FavoritesContext";
import RootDrawer from "./routes/RootDrawer";
import { GestureHandlerRootView } from "react-native-gesture-handler";
import "react-native-gesture-handler";
import { SQLiteProvider } from "expo-sqlite";

async function initDb(db) {
  await db.execAsync(`
    CREATE TABLE IF NOT EXISTS notes (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      recipeId TEXT NOT NULL,
      text TEXT NOT NULL,
      createdAt TEXT DEFAULT (datetime('now'))
    );

    CREATE TABLE IF NOT EXISTS openCounts (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      recipeId TEXT NOT NULL UNIQUE,
      count INTEGER NOT NULL DEFAULT 0
    );
  `);
}

export default function App() {
  return (
    <SQLiteProvider databaseName="recipes.db" onInit={initDb}>
      <GestureHandlerRootView>
        <FavoritesProvider>
          <NavigationContainer>
            <RootDrawer />
          </NavigationContainer>
        </FavoritesProvider>
      </GestureHandlerRootView>
    </SQLiteProvider>
  );
}
