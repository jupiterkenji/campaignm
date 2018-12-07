var expect = require('chai').expect;
var arrangeBy = require('../arrangeBy');

describe('arrangeBy()', function() {
    it ('should arrangeBy', function() {

        const input = [{
            id: 1,
            name: 'bob',
        }, {
            id: 2,
            name: 'sally',
        }, {
            id: 3,
            name: 'bob',
            age: 30,
        }]

        var expectedOutput = {
            bob: [{
                id: 1,
                name: 'bob'
            }, {
                id: 3,
                name: 'bob',
                age: 30
            }],
            sally: [{
                id: 2,
                name: 'sally'
            }]
        }

        const arrangeByName = arrangeBy('name')
        var output = arrangeByName(input)

        // console.log('\r\noutput: \r\n')
        // console.log(output);
        // console.log('\r\n---------------------------------\r\n')
        // console.log('\r\expectedOutput: \r\n')
        // console.log(expectedOutput);

        //expect(output).to.be.equal(expectedOutput);
        expect(output).to.deep.equal(expectedOutput);
    })
});