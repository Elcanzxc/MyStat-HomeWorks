import { View,Text,StyleSheet } from "react-native";




function ProfileCard({name,city,progLanguage,style,dx}){


    return (

        <View style={[styles.card,style]}>

            <Text>My name is: {name} , and I live in {city} </Text>
            <Text>My favorite programming language is :{progLanguage}</Text>

        </View>
    )
}


const styles = StyleSheet.create({
     
    card:{
        borderWidth:2,
        borderColor: 'black',
        backgroundColor: 'yellow'
    }
})
export default ProfileCard