import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
const PersonProfile = Loadable(lazy(() => import('../views/Person/Profile')));
const CategoryEdit = Loadable(lazy(() => import('../views/Category/Edit')));
// const PersonCreate = Loadable(lazy(() => import('../views/Person/Create')));
const CreateCategory = Loadable(lazy(() => import('../views/Category/Create')));
const IndexCategory = Loadable(lazy(() => import('../views/Category')));

const CategoryRoute = {
    path: '/category',
    element: <MainLayout/>,
    children: [
        {
            path: '/category',
            element: <IndexCategory/>
        },
        {
            path: '/category/:id',
            element: <PersonProfile/>
        },
        {
            path: '/category/create',
            element: <CreateCategory/>
        },
        {
            path: '/category/:id/edit',
            element: <CategoryEdit/>
        },
    ]
 };

export default CategoryRoute;
