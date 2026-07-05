import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";

import { FavoritesProvider } from "./context/FavoritesContext";
import RootDrawer from "./routes/RootDrawer";
import "react-native-gesture-handler";
import { GestureHandlerRootView } from "react-native-gesture-handler";

export default function App() {
  return (
    <GestureHandlerRootView>
      <FavoritesProvider>
        <NavigationContainer>
          <RootDrawer />
        </NavigationContainer>
      </FavoritesProvider>
    </GestureHandlerRootView>
  );
}
