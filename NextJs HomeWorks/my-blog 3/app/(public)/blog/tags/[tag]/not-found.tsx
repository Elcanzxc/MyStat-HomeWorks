import styles from "./not-found.module.css";


export default function NotFoundPage(){

return (

<div className={styles.wrapper}>

<h1 className={styles.title}>
404
</h1>


<p className={styles.text}>
Страница под таким айди не найдена!
</p>


</div>

)

}