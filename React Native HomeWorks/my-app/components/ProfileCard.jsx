
import { Button, View ,Text, StyleSheet,Image, Pressable} from "react-native"
import { useState  } from "react"

export default function ProfileCard({name,role,url}){
   const [active, setActive] = useState(false)

    return (

   <Pressable onPress={() => {setActive(!active)}}>
        <View style={[styles.card , {borderColor:active? 'green':'black'}]}>

            
              
               <Text> {name}</Text>
               <Text> {role}</Text>
               <Image source={{ uri: url }} style={styles.image} />
               {active ? <Text>✓ Выбрано</Text>: <Text> </Text>}

            
      
        </View>
    </Pressable>
    )
}

const styles = StyleSheet.create({
  card: {
    borderWidth: 2,
    padding: 25,
    paddingVertical: 50,
    borderRadius: 20,
    borderColor: "black",
  },

  image: {
    width: 100,
    height: 100,
  },
});