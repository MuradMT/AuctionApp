import type { Metadata } from 'next'
import './globals.css'
import Navbar from './components/navcomponents/Navbar'
import ToasterProvider from './providers/ToasterProvider'
import { getCurrentUser } from './server/authAuctions'
import SignalRProvider from './providers/SignalRProvider'



export const metadata: Metadata = {
  title: 'Auction of Azerbaijan',
  description: 'Generated by create next app',
  icons:
  {
    icon:['/favicon.ico?v=4']
  }    
  
}

export default async function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  const user = await getCurrentUser();
  return (
    <html lang="en">
      <body>
      <ToasterProvider/>
      <Navbar/>
      <main className='container mx-auto px-5 pt-10'>
      <SignalRProvider user={user}>
            {children}
          </SignalRProvider>
      </main>
        </body>
    </html>
  )
}
