
import { Button, View ,Text, StyleSheet} from "react-native"
import { useState  } from "react"

export default function Like(){
    
    const [likes,setLikes] = useState (0)


    return (
        <View>
             <Button title="Поставить лайк ❤️" onPress={() => {setLikes(likes+1)}}/>
             <Text style={styles.like}> [{likes}]</Text>
        </View>
    )
}

const styles = StyleSheet.create({
    like:{
        fontSize:30
    }
})