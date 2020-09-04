import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export default function newItemGroupReducer(
  state = initialState.newItemGroup,
  action
) {
  switch (action.type) {
    case types.NEW_ITEM_GROUP_SUCCESS:
      return action.newItemGroup;
    default:
      return state;
  }
}
