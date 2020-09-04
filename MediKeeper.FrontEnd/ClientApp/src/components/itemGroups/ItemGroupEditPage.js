import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as itemGroupActions from "../../redux/actions/itemGroupActions";
import PropTypes from "prop-types";

function ItemGroupEditPage({
  id,
  selectedItemGroup,
  loadSelectedItemGroup,
  saveItemGroup,
  history,
}) {
  useEffect(() => {
    async function fetchData() {
      try {
        await loadSelectedItemGroup(id);
      } catch (error) {
        alert("Failed to load itemGroup. " + error);
      }
    }
    fetchData();
  }, [id, loadSelectedItemGroup]);

  const [name, setName] = useState(selectedItemGroup.name);
  const [cost, setCost] = useState(selectedItemGroup.cost);

  const onSaveClickedHandler = async (event) => {
    event.preventDefault();
    const updatedItemGroup = {
      ...selectedItemGroup,
      name,
      cost,
    };
    await saveItemGroup(updatedItemGroup);
    history.push("/itemGroups/" + updatedItemGroup.id);
  };

  const onCancelClickedHandler = (event) => {
    event.preventDefault();
    history.push("/itemGroups/" + selectedItemGroup.id);
  };

  return (
    <div className="row justify-content-center">
      <div className="col-6 mt-5">
        <form>
          <h1>Edit {selectedItemGroup.name}</h1>
          <div className="form-group">
            <label htmlFor="itemName">Item Name</label>
            <input
              type="text"
              className="form-control"
              id="itemName"
              aria-describedby="itemName"
              placeholder="Enter name"
              defaultValue={name}
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
              defaultValue={cost}
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

ItemGroupEditPage.propTypes = {
  itemGroup: PropTypes.object.isRequired,
  loadSelectedItemGroup: PropTypes.func.isRequired,
  saveItemGroup: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
};

const mapStateToProps = (state, ownProps) => {
  return {
    id: ownProps.match.params.id,
    selectedItemGroup: state.selectedItemGroup,
  };
};

const mapDispatchToProps = {
  loadSelectedItemGroup: itemGroupActions.loadSelectedItemGroup,
  saveItemGroup: itemGroupActions.saveItemGroup,
};

export default connect(mapStateToProps, mapDispatchToProps)(ItemGroupEditPage);
