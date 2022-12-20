import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
const PersonProfile = Loadable(lazy(() => import('../views/Person/Profile')));
const PersonEdit = Loadable(lazy(() => import('../views/Person/Edit')));
// const PersonCreate = Loadable(lazy(() => import('../views/Person/Create')));
const PersonCreate = Loadable(lazy(() => import('../views/Person/CreateSimple')));
const IndexPeople = Loadable(lazy(() => import('../views/Person')));

const PersonRoutes = {
    path: '/person',
    element: <MainLayout/>,
    children: [
        {
            path: '/person',
            element: <IndexPeople/>
        },
        {
            path: '/person/:id',
            element: <PersonProfile/>
        },
        {
            path: '/person/createSimple',
            element: <PersonCreate/>
        },
        {
            path: '/person/:id/edit',
            element: <PersonEdit/>
        },
    ]
 };

export default PersonRoutes;
