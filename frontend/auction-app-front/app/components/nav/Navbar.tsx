import React from "react";
import Search from "./Search";
import Logo from "./Logo";
const Navbar = () => {
  return (
    <>
      <header
        className="
          sticky top-0 z-50 flex justify-between
          bg-white p-5 items-center text-cyan-300 shadow-md"
      >
        <Logo/>
        <Search/>
        <div className="Navbar__Header__Right">Login</div>
      </header>
    </>
  );
};

export default Navbar;
