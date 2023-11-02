import React from 'react'
import { getSession } from '../server/authAuctions'
import Heading from '../components/shared/Heading';

const page = async () => {
    const session=await getSession();
  return (
    <div>
        <Heading title='Session dashboard' />
        <div className='bg-blue-200 border-2 border-blue-500'>
              <h3 className='text-lg'>
                   Session data
              </h3>
              <pre>{JSON.stringify(session,null,2)}</pre>
        </div>
    </div>
  )
}

export default page