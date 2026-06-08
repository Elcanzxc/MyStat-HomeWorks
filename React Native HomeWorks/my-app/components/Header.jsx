
import { Button, View ,Text, StyleSheet,Image, Pressable} from "react-native"
import { useState  } from "react"

export default function Header({title}){
   const [active, setActive] = useState(false)

    return (
        <View style={styles.card }>
                <Text>{title}</Text>
        </View>
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

});