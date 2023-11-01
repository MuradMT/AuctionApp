'use client'
import { useParamsStore } from '@/hooks/useParamsStore'
import React, { useState } from 'react'
import {FaSearch} from 'react-icons/fa'
const Search = () => {
    const setParams=useParamsStore(state=>state.setParams)
    const setSearchValue=useParamsStore(state=>state.setSearchValue)
    const searchValue=useParamsStore(state=>state.searchValue)
    function onChange(event:any){
        setSearchValue(event.target.value)
    }
    function search(){
      console.log('hi')
        setParams({searchTerm:searchValue})
    }
  return (
    <div className='flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm'>
             <input
             onKeyDown={(e:any)=>{
                if(e.key==='Enter') search()
             }} 
             value={searchValue}
             onChange={onChange}
             type='text' 
             placeholder='Search of Auction' //only held make model color
             className='
                 flex-grow
                 pl-5
                 bg-transparent
                 focus:outline-none
                 border-transparent
                 focus:border-transparent
                 focus:ring-0
                 text-sm
                 text-gray-600
                 font-semibold
             '
             />
             <button onClick={search} title='search'>
                <FaSearch size={34} className='bg-violet-600 text-white rounded-full p-2 cursor-pointer mx-2'/>
             </button>
    </div>
  )
}

export default Search