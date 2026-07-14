

export const revalidate = 15

export default function DemoRandomPage() {
  const randomNumber = Math.random()
  return (
    <div>
      <h1>Демо: случайное число + {revalidate}</h1>
      <p>Случайное число {randomNumber}</p>
    </div>
  )
}