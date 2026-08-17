import { error } from "console"


export async function fetcher(url:string){
    const res = await fetch(url)

    if(!res){
        throw new Error('Error')
    }

    return await res.json()
}