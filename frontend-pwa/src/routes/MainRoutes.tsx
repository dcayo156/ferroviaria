import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../ui-component/Loadable';
import SearchPage from '../views/SearchPage/Index';
const Home = Loadable(lazy(() => import('../views/Dashboard/Home')));
const SearchAddress = Loadable(lazy(() => import('../views/SearchAddress/SearchAddress')));
const MainRoutes = {
    path: '/main',
    element: <MainLayout/>,
    children: [
        {
            path: '/main/home',
            element: <SearchPage/>
        },
        {
            path: '/main/search-address',
            element: <SearchAddress />
        }
    ]
 };

export default MainRoutes;
