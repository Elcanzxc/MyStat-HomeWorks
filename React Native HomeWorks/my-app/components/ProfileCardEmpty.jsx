import { StyleSheet, View,Text, Image } from "react-native";




export default function ProfileCardEmpty(){
    return(
        <View style={styles.card}> 
           <Text style={styles.text}>Pusto</Text>
        </View>
    )
}

const styles = StyleSheet.create({
    card:{
      borderWidth:1,
      width:160,
      height:180,
      borderColor:'#034569',
      backgroundColor:'#086CA2',
      justifyContent:'center',
      alignItems:'center',
      borderRadius:15,
    },
    text:{
     fontSize:25,
     color:'white'
    }
})