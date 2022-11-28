import { useRoutes } from 'react-router-dom';
import MainRoutes from './MainRoutes';
import PersonRoutes from './PersonRoutes';
import TagCategory from './TagCategory';
import AuthenticationRoutes from './AuthenticationRoutes';
import { lazy } from 'react';
import Loadable from '../ui-component/Loadable';
import MinimalLayout from '../layout/MinimalLayout';
import ReltaionRoutes from './ReltionshipRoutes';
import AuthRoutes from './AuthRoutes';
import UserRoutes from './UserRoutes';

const Login = Loadable(lazy(() => import('../components/Security/Login')));

export default function ThemeRoutes() {
    return useRoutes([{
        path: '/',
        element: <MinimalLayout />,
        children: [
            {
                path: '/',
                element: <Login />
            }
        ]
    },
    MainRoutes,
    AuthenticationRoutes,
    PersonRoutes,
    ReltaionRoutes,
    TagCategory,
    AuthRoutes,
    UserRoutes]);
}
