import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Form, FormGroup, Input, Label } from 'reactstrap';

import { setParam } from '../actions';

export class Filter extends Component {
    constructor(props) {
        super(props);

        const { productList, param } = window.initialState;

        this.productList = productList;
        this.param = param;

        this.state = {
            selectedProduct: 0
        }
    }

    componentDidMount() {
        this._submit(this.state.selectedProduct);
    }

    handleChangeProduct = (event) => {
        const selectedProduct = Number(event.target.value);

        this.setState({ selectedProduct });

        this._submit(selectedProduct);
    }

    _submit(selectedProduct) {
        const param = Object.assign(this.param, { ProductId: selectedProduct });

        this.props.setParam(param);
    }

    render() {
        const { selectedProduct } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-2">Search By</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">All</option>
                            {
                                this.productList.map(p => {
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

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(null, mapDispatchToProps)(Filter);