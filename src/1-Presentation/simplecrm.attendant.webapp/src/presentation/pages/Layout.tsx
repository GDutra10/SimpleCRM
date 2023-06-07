import {Link, Outlet } from "react-router-dom";

function Layout(){
    return (
        <>
            <nav className="navbar">
                <a className="navbar-brand">Simple CRM - Attendant</a>
                <ul className="navbar-nav">
                    <li className="nav-item">
                        <Link to="/" className="nav-link">Home</Link>
                    </li>
                </ul>
            </nav>
            <div className="App">
                <Outlet />
            </div>
        </>
    );
}

export default Layout;