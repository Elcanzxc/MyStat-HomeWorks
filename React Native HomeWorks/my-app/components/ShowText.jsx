
import { Button, View ,Text, StyleSheet} from "react-native"
import { useState  } from "react"

export default function ShowText(){
    
    const [showText, setVisibility] = useState (true)


    return (
        <View>
             <Button title={showText? "Скрыть текст":"Показать текст"} onPress={() => {setVisibility(!showText)}}/>
             
             {showText? 
              <Text> Привет , это текст который ты должен увидеть</Text>:
              <Text></Text>
            }
        </View>
    )
}

const styles = StyleSheet.create({
    like:{
        fontSize:30
    }
})