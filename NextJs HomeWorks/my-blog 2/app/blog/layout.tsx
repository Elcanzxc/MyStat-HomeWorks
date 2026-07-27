"use client";

import {ReactNode} from "react";
import {useRouter} from "next/navigation";
import styles from "./layout.module.css";


export default function LayoutPage({
children
}:{
children:ReactNode
}){


const router=useRouter();



return (

<div className={styles.wrapper}>

<button
className={styles.back}
onClick={()=>router.back()}
>

← Вернуться назад

</button>


{children}


</div>

)

}