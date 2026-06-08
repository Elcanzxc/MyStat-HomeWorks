
import { Button, View ,Text, StyleSheet} from "react-native"
import { useState  } from "react"

export default function Count(){
    
    const [count,setCount] = useState (0)
    
    let changeCount = (digit) =>{
        const newValue = count + digit;

        if (newValue < 0) return;

        setCount(newValue);
     
    }

    return (
        <View style={styles.container}> 
             <Button title="+" onPress={() => {changeCount(1)}}/>
             <Text style={styles.count}> [{count}]</Text>
             <Button title="-" onPress={() => {changeCount(-1)}}/>
             <Button title="Сбросить" onPress={() => {setCount(0)}}/>

        </View>
    )
}

const styles = StyleSheet.create({
    count:{
        fontSize:30
    },
    container:{
       gap: 15, 
    }
})