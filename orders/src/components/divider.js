import React, { Component } from 'react'
import { connect } from "react-redux"
import { menuChoice, getProducts, setCategory } from "../dispatcher/actions"
import '../App.css'

class Divider extends Component {
    constructor() {
        super();

        this.state = {
            category: "all"
        }

        this.goToCatalog = this.goToCatalog.bind(this)
        this.filter = this.filter.bind(this)
    }

    goToCatalog() {
        let viewInstructions = {
            catalogView: true,
            productView: false,
            ordersView: false
        }

        let instructions = {
            viewAmt: 10,
            startVal: 0,
            category: this.state.category
        }

        this.setState(instructions.category)

        menuChoice(this.props.dispatch, viewInstructions)
        getProducts(this.props.dispatch, instructions)
        setCategory(this.props.dispatch, instructions.category)
    }

    changeInputs = e => {
        this.setState({ category: e.target.value })

        console.log(this.state.category)

        this.filter(e.target.value)
    }


    filter(c) {
        let instructions = {
            viewAmt: 10,
            startVal: 0,
            category: c
        }

        console.log(c)

        setCategory(this.props.dispatch, c)
        getProducts(this.props.dispatch, instructions)
    }


    render() {
        return (

            <div className="divider">

                {this.props.catalogView ?

                    <div className="divider-inner">

                        <select onChange={this.changeInputs}>
                            <option name="category" value="all">ALL PRODUCTS:</option>
                            <option name="category" value="Parts & Bits">PARTS &amp; BITS</option>
                            <option name="category" value="Presses">PRESSES</option>
                            <option name="category" value="Beveling Tools">BEVELING TOOLS</option>
                            <option name="category" value="Hand Tools">HAND TOOLS</option>

                        </select>

                    </div>

                    : ""}

            </div>


        );
    }
}
export default connect(
    store => ({

        /*         productList: store.productList,
                productCount: store.productCount,
                productDetails: store.productDetails,
                productView: store.productView, */
        catalogView: store.catalogView,

        category: store.category
    })
)(Divider);
