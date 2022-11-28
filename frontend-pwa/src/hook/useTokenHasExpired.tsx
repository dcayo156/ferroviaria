
import { getTokenExp } from '../store/slices/Auth/localStorage';
const tokenHasExpired=(exp:number):boolean=>{
  if(exp===0)return false;
  var now = new Date();
  var dateExp = new Date(exp);
  return now.getTime() < dateExp.getTime();
}
export const useTokenHasExpired = () => {
  const o=tokenHasExpired(getTokenExp());
  return o
}
