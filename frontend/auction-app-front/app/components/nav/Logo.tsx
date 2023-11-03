'use client'
import { useParamsStore } from '@/hooks/useParamsStore'
import { usePathname, useRouter } from 'next/navigation'
import React from 'react'
import { RiAuctionFill } from 'react-icons/ri'

const Logo = () => {
    const router=useRouter()
    const pathname=usePathname()
    const reset=useParamsStore(state=>state.reset)
    function doReset(){
         if(pathname !=='/') router.push('/')
         reset();    }
  return (
    <div onClick={doReset} className="cursor-pointer flex items-center gap-2 text-3xl font-bold text-violet-600">
            <RiAuctionFill size={34}/>
            <div>Auction of Azerbaijan</div>
        </div>
  )
}

export default Logo