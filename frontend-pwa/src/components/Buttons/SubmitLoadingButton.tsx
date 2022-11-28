import { Box, Button, CircularProgress, useTheme } from '@mui/material';
import * as React from 'react';
interface SubmitLoadingButtonProps {
    loading:boolean
    text:string
    fullWidth:boolean
}
 
const SubmitLoadingButton: React.FunctionComponent<SubmitLoadingButtonProps> = ({loading,text,fullWidth}) => {
    const theme=useTheme();
    return ( <Box sx={{ m: 1, position: 'relative' }}>
    <Button
        type="submit"
        fullWidth={fullWidth}
      variant="contained"
      disabled={loading}
    >
      {text}
    </Button>
    {loading && (
      <CircularProgress
        size={24}
        sx={{
          color: theme.palette.primary.main,
          position: 'absolute',
          top: '50%',
          left: '50%',
          marginTop: '-12px',
          marginLeft: '-12px',
        }}
      />
    )}
  </Box> );
}
 
export default SubmitLoadingButton;