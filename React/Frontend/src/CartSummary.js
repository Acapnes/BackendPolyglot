import React, { Component } from "react";

import {
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  Badge,
} from "reactstrap";

import { Link } from "react-router-dom";

export default class CartSummary extends Component {
  render() {
    return (
      <div>
        <UncontrolledDropdown nav inNavbar>
          <DropdownToggle nav caret>
            Your Cart - {this.props.cart.length}
          </DropdownToggle>
          <DropdownMenu right>
            {this.props.cart.map((cartItem) => (
              <DropdownItem key={cartItem.product.id}>
                {cartItem.product.productName}
                <Badge color="success">{cartItem.quantity}</Badge>
              </DropdownItem>
            ))}
            <DropdownItem divider />
            <DropdownItem>
              <Link to="cart">Go to cart</Link>
            </DropdownItem>
          </DropdownMenu>
        </UncontrolledDropdown>
      </div>
    );
  }
}
