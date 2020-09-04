import React from "react";
import { Route, Switch } from "react-router-dom";
import HomePage from "./components/home/HomePage";
import Header from "./components/common/Header";
import ItemGroupsPage from "./components/itemGroups/ItemGroupsPage";
import ManageItemGroupPage from "./components/itemGroups/ManageItemGroupPage";
import ItemGroupEditPage from "./components/itemGroups/ItemGroupEditPage";
import ItemGroupAddPage from "./components/itemGroups/ItemGroupAddPage";

function App() {
  return (
    <>
      <Header />
      <div className="container">
        <Switch>
          <Route exact path="/" component={HomePage} />
          <Route exact path="/ItemGroups/Add" component={ItemGroupAddPage} />
          <Route path="/ItemGroups/:id/Edit" component={ItemGroupEditPage} />
          <Route path="/ItemGroups/:id" component={ManageItemGroupPage} />
          <Route path="/ItemGroups" component={ItemGroupsPage} />
        </Switch>
      </div>
    </>
  );
}

export default App;
