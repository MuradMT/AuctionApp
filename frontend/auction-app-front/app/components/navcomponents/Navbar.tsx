import React from "react";
import Search from "./Search";
import Logo from "./Logo";
import LoginButton from "./LoginButton";
import { getCurrentUser } from "@/app/server/authAuctions";
import UserActions from "./UserActions";
const Navbar = async () => {
  const user=await getCurrentUser();
  return (
    <>
      <header
        className="
          sticky top-0 z-50 flex justify-between
          bg-white p-5 items-center text-violet-600 shadow-md font-bold"
      >
        <Logo/>
        <Search/>
        {
          user?(
            <UserActions user={user}/>
          ):(
            <LoginButton/>
          )
        }
      </header>
    </>
  );
};

export default Navbar;
