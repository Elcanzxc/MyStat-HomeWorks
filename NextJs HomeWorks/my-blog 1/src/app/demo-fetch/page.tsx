import React from 'react'

const Page = async () => {
  const resSSG = await fetch('https://rickandmortyapi.com/api/character') //SSG
  const resSSR = await fetch('https://rickandmortyapi.com/api/location', {
    cache: 'no-store'
  })
  const resISR = await fetch('https://rickandmortyapi.com/api/episode', {
    next: { revalidate: 60 }
  })
  console.log(resISR)
  return (
    <div>Page</div>
  )
}

export default Page