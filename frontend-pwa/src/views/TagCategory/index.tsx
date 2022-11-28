import * as React from 'react';
import { useGetTagCategoryQuery } from '../../store/services/Tag'
import MainCard from '../../components/cards/MainCard'
import CardButton from '../../components/cards/CardButton';
import Table from './Table'
import { LinearProgress } from '@mui/material';
interface TagCategoryProps {

}

const TagCategory: React.FunctionComponent<TagCategoryProps> = () => {
    const { data: categorires, error, isLoading } = useGetTagCategoryQuery();
    return (
        <MainCard title="Categoria de Tags" secondary={null} >
            {
                isLoading?
                <LinearProgress color="secondary" />
                :
                <Table categories={categorires!=undefined ? categorires: []} key="TagCategories"/>
            }
            
        </MainCard>
        );
}

export default TagCategory;
