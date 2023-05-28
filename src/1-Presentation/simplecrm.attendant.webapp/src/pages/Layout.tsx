import {Link, Outlet } from "react-router-dom";

function Layout(){
    return (
        <>
            <nav className="menu">
                <ul>
                    <li><Link to="/">Home</Link></li>
                </ul>
            </nav>
            <div className="App">
                <Outlet />
            </div>
        </>
    );
}

export default Layout;