import { View , StyleSheet ,ScrollView} from "react-native";
import ProfileCard from "./components/ProfileCard"


function App(){


  return (
    <ScrollView contentContainerStyle={styles.container}> 
    
      
      <ProfileCard  name="Elcan" city="Baku" progLanguage="C#"></ProfileCard>
      <ProfileCard style={styles.card} name="Unknown" city="Unknown" progLanguage="Unknown"></ProfileCard>
       <ProfileCard  name="Unknown2" city="Unknown2" progLanguage="Unknown2"></ProfileCard>

    </ScrollView>
  )
}


export default App

const styles = StyleSheet.create(
    {
      container:{
         flexGrow: 1,
         justifyContent: 'center',
         alignItems: 'center',
         backgroundColor: '#f5f5f5',
      },
      card:{
        borderWidth:2,
        borderColor: 'black',
        backgroundColor: 'green'
      }
    }
)
