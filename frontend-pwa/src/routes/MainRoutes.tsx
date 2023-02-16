import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';

const Home = Loadable(lazy(() => import('../views/Dashboard/Home')));
const rutaServidor = "";//PRUBEA
//const rutaServidor = "/FO"; //Produccion
const MainRoutes = {
    path: '/main',
    element: <MainLayout/>,
    children: [
        {
            path:  '/main/home',
            element: <Home/>
        }
    ]
 };

export default MainRoutes;
