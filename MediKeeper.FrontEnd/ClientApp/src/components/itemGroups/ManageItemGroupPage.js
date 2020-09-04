import React, { useEffect } from "react";
import { connect } from "react-redux";
import * as itemGroupActions from "../../redux/actions/itemGroupActions";
import PropTypes from "prop-types";
import ItemGroupDetails from "./ItemGroupDetails";
import { faTrash, faPencilAlt } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";

function ManageItemGroupPage({
  id,
  selectedItemGroup,
  loadSelectedItemGroup,
  deleteItemGroup,
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

  const onDeleteClickedHandler = () => {
    confirmAlert({
      title: "Deletions cannot be undone.",
      message: "Are you sure you want to delete this item?",
      buttons: [
        {
          label: "Yes",
          onClick: () => {
            deleteItemGroup(id);
            history.push("/itemGroups");
          },
        },
        {
          label: "No",
          onClick: () => {},
        },
      ],
    });
  };

  const onEditClickedHandler = () => {
    history.push("/itemGroups/" + id + "/edit");
  };

  return (
    <>
      <div className="row mt-5">
        <div className="col">
          <h1>{selectedItemGroup.name}</h1>
        </div>
        <div className="col">
          <div className="float-right m-2">
            <button
              type="button"
              className="btn btn-danger ml-1"
              onClick={onDeleteClickedHandler}
            >
              <FontAwesomeIcon
                icon={faTrash}
                style={{ width: 25, height: 25 }}
              />
            </button>
            <button
              type="button"
              className="btn btn-primary ml-1"
              onClick={onEditClickedHandler}
            >
              <FontAwesomeIcon
                icon={faPencilAlt}
                style={{ width: 25, height: 25 }}
              />
            </button>
          </div>
        </div>
      </div>

      {selectedItemGroup ? (
        <ItemGroupDetails itemGroup={selectedItemGroup} />
      ) : (
        <p>Loading...</p>
      )}
    </>
  );
}

ManageItemGroupPage.propTypes = {
  itemGroup: PropTypes.object.isRequired,
  loadSelectedItemGroup: PropTypes.func.isRequired,
};

const mapStateToProps = (state, ownProps) => {
  return {
    id: ownProps.match.params.id,
    selectedItemGroup: state.selectedItemGroup,
  };
};

const mapDispatchToProps = {
  loadSelectedItemGroup: itemGroupActions.loadSelectedItemGroup,
  deleteItemGroup: itemGroupActions.deleteItemGroup,
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ManageItemGroupPage);
