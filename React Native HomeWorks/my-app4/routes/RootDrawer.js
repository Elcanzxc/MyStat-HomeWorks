import { createDrawerNavigator} from "@react-navigation/drawer"
import { NavigationContainer } from "@react-navigation/native"
import AboutScreen from "../screens/AboutScreen"
import Tabs from "./Tabs";

const Drawer = createDrawerNavigator()

const RootDrawer = () => {
 
     return (
        <Drawer.Navigator>
            <Drawer.Screen name='Main' component={Tabs} options={{headerShown:true}} />
            <Drawer.Screen name='About' component={AboutScreen} options={{headerShown:false}} />
        </Drawer.Navigator>

     )
}

export default RootDrawer