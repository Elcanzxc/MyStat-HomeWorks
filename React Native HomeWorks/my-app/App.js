import { View , StyleSheet ,ScrollView,Text} from "react-native";
import Like from "./components/Like";
import ShowText from "./components/ShowText";
import ProfileCard from "./components/ProfileCard";
import Count from "./components/Count";
import Header from "./components/Header";


function App(){


  return (
    <ScrollView contentContainerStyle={styles.scrollContainer}>
     <View style={styles.container}>
           <Like/>
           <ShowText/>
           <ProfileCard name='User' url='https://i.pravatar.cc/150?u=a042581f4e29026704d'/>
           <Count/>
           

            {/* Второе Задание */}
           <Text style={{paddingTop:50}}>Второе Задание</Text>
           <Header title='Elcan Team'/>
           <ProfileCard name='Elcan' role='Backend' url='https://i.pravatar.cc/300'/>
           <ProfileCard name='Bayramov' role='FrontEnd' url='https://i.pravatar.cc/303'/>


     </View>
     </ScrollView>
  )
}


export default App

const styles = StyleSheet.create({
  
    container:{

       flex:1,
       flexDirection:'column',
       alignItems:'center',
       justifyContent:'center',
       gap:10

    },

    scrollContainer:{
      paddingTop: 60,
      paddingBottom: 100,
      alignItems: "center",
      gap: 10,
    }
    
  })
