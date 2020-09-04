import { combineReducers } from "redux";
import itemGroups from "./itemGroupsReducer";
import itemGroupsPagination from "./itemGroupsPaginationReducer";
import selectedItemGroup from "./selectedItemGroupReducer";
import newItemGroup from "./newItemGroupReducer";

const rootReducer = combineReducers({
  itemGroups,
  itemGroupsPagination,
  selectedItemGroup,
  newItemGroup,
});

export default rootReducer;
