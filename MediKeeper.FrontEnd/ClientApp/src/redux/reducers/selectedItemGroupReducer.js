import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export default function selectedItemGroupReducer(
  state = initialState.selectedItemGroup,
  action
) {
  switch (action.type) {
    case types.LOAD_SELECTED_ITEM_GROUP_SUCCESS:
      return action.selectedItemGroup;
    default:
      return state;
  }
}
