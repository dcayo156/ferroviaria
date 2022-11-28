import * as React from 'react';
import { Box, Typography, Checkbox, useTheme, Button } from '@mui/material'
import { ICategoryData } from "../../../store/types/Category";
interface AcordionCategoryProps {
    category: ICategoryData;
    setCategory: (tagId: string, tagCategoryId: string) => void;
}

const AcordionCategory: React.FunctionComponent<AcordionCategoryProps> = ({ category, setCategory}) => {
    const theme = useTheme();
    const [isExpanded, setIsExpanded] = React.useState(true);
    const description = "0"
    const change = () => {
        setIsExpanded(!isExpanded);
    }
    const handleChange = (tagId: string, tagCategoryId: string) => {
        setCategory(tagId, tagCategoryId);
    };

    const handleExpander = () => {
        setIsExpanded(!isExpanded)
    }
    return (<Box sx={{ diplay: "flex", flexDirection: "column", padding: "5px 0 5px 5px", boxShadow: `1px 1px 2px 0px ${theme.palette.secondary.main}` }}>
        <Box style={{ cursor: "pointer" }} onClick={change}>
            <Typography variant='h6'>{category.name}</Typography>
        </Box>
        <Box style={{ display: isExpanded ? "" : "none", borderTop: `1px solid ${theme.palette.primary.dark}`, padding: "5px 0 0 5px" }}>
            {category.tags.map((item) => (
                <Box display="flex"  key={item.id}>
                    <Checkbox
                        id={`id${item.id}`}
                        sx={{ p: "2px" }}
                        size="small"
                        checked={item.selected}
                        onChange={() => handleChange(item.id, category.id)}
                        name={item.id}
                    />
                    <label htmlFor={`id${item.id}`} style={{ paddingTop: "4px", display: "flex" }}>
                        {item.name}
                        <div style={{ paddingLeft: ".5em", paddingTop: ".2em", fontSize: ".8em", color: theme.palette.primary.dark }}>
                            ({item.numberOfPeople})
                        </div>
                    </label>
                </Box>
            ))}

        </Box>
    </Box>);
}

export default AcordionCategory;