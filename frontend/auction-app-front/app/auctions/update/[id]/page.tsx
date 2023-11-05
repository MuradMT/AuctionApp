import AuctionForm from '@/app/components/auctioncomponents/AuctionForm'
import Heading from '@/app/components/sharedcomponents/Heading'
import { getDetailedViewData } from '@/app/server/auctionActions'
import React from 'react'

const Update = async ({params}:{params:{id:string}}) => {
  const data=await getDetailedViewData(params.id);
  return (
    <div className='mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
         <Heading title='Update your auction' subtitle='Please update the details of your car'/>
         <AuctionForm auction={data}/>
    </div>
  )
}

export default Update