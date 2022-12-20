import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
const IndexTagCategory = Loadable(lazy(() => import('../views/TagCategory')));

const TagCategoryRoutes = {
    path: '/tag-category',
    element: <MainLayout/>,
    children: [
        {
            path: '/tag-category',
            element: <IndexTagCategory/>
        },
    ]
 };

export default TagCategoryRoutes;
