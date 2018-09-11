import React, { Component } from 'react';

import { Form, FormGroup, Input, Label } from 'reactstrap';

export default class Filter extends Component {
    constructor(props) {
        super(props);
    }

    handleChangeProduct = (event) => {
        const selectedProduct = Number(event.target.value);

        this.props.onChangeProduct(selectedProduct);
    }

    render() {
        const { productList, selectedProduct } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">All</option>
                            {
                                productList.map(p => {
                                    return (<option value={p.ProductId}>{p.ProductName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}