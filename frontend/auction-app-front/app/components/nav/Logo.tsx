'use client'
import { useParamsStore } from '@/hooks/useParamsStore'
import React from 'react'
import { RiAuctionFill } from 'react-icons/ri'

const Logo = () => {
    const reset=useParamsStore(state=>state.reset)
  return (
    <div onClick={reset} className="cursor-pointer flex items-center gap-2 text-3xl font-semibold text-violet-600">
            <RiAuctionFill size={34}/>
            <div>Auction of Azerbaijan</div>
        </div>
  )
}

export default Logo