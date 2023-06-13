import {Link, Outlet } from "react-router-dom";
import {LoginHelper} from "../../domain/helpers/LoginHelper";

function Layout(){
    return (
        <>
            <nav className="navbar">
                <a className="navbar-brand">Simple CRM - Attendant</a>
                <ul className="navbar-nav">
                    <li className="nav-item">
                        <Link to="/" className="nav-link">Home</Link>
                    </li>
                    <li className="nav-item">
                        <a href="#" className="nav-link" onClick={e => { LoginHelper.Logout(); }}>Logout</a>
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