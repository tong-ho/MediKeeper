import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import * as itemGroupActions from "../../redux/actions/itemGroupActions";
import PropTypes from "prop-types";

function ItemGroupAddPage({
  saveItemGroup,
  history,
  newItemGroup,
  setNewItemGroup,
}) {
  useEffect(() => {
    if (newItemGroup.id) {
      const id = newItemGroup.id;
      setNewItemGroup({});
      history.push("/itemGroups/" + id);
    }
  }, [newItemGroup]);

  const [name, setName] = useState();
  const [cost, setCost] = useState();

  const onSaveClickedHandler = async (event) => {
    event.preventDefault();
    const updatedItemGroup = {
      name,
      cost,
    };
    await saveItemGroup(updatedItemGroup);
    history.push("/itemGroups/" + updatedItemGroup.id);
  };

  const onCancelClickedHandler = (event) => {
    event.preventDefault();
    history.push("/itemGroups");
  };

  return (
    <div className="row justify-content-center">
      <div className="col-6 mt-5">
        <form>
          <h1>Add</h1>
          <div className="form-group">
            <label htmlFor="itemName">Item Name</label>
            <input
              type="text"
              className="form-control"
              id="itemName"
              aria-describedby="itemName"
              placeholder="Enter name"
              onChange={(event) => {
                setName(event.target.value);
              }}
            />
          </div>
          <div className="form-group">
            <label htmlFor="itemCost">Cost</label>
            <input
              type="number"
              className="form-control"
              id="itemCost"
              placeholder="0.00"
              onChange={(event) => {
                setCost(event.target.value);
              }}
            />
          </div>
          <button
            type="submit"
            className="btn btn-primary float-right"
            onClick={onSaveClickedHandler}
          >
            Save
          </button>
          <button
            type="submit"
            className="btn btn-danger float-right mr-2"
            onClick={onCancelClickedHandler}
          >
            Cancel
          </button>
        </form>
      </div>
    </div>
  );
}

ItemGroupAddPage.propTypes = {
  saveItemGroup: PropTypes.func.isRequired,
  setNewItemGroup: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
};

const mapStateToProps = (state, ownProps) => {
  return {
    newItemGroup: state.newItemGroup,
  };
};

const mapDispatchToProps = {
  saveItemGroup: itemGroupActions.saveItemGroup,
  setNewItemGroup: itemGroupActions.setNewItemGroup,
};

export default connect(mapStateToProps, mapDispatchToProps)(ItemGroupAddPage);
