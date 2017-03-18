# LanguagePatternsAndExtensions
basic language adaptations I use commonly in Web API projects

I've added an Outcomes class which serves as a light "Either" monad.  However, I'm sticking pretty close to object oriented
principals so I'm also not going down the functional rabbit hole.  Basically, when I build operations in WebApi, I use CQS
and I've added interfaces to model commands and queries.  These interfaces force implementers into pipelining their work
through Tasks and Outcomes.  Really, when I'm working in webapi, that's all I care about... cause I wake up in the morning
and do something in SQL... and then when I go to bed at night, I send to a javascript promise on either success or failure
track.  It makes for a predictable API from the JS side, whether you're attaching callbacks in JQuery or chaining promises
in angular.

Credit goes to
https://github.com/louthy/language-ext/
published under MIT at the time for the Unit struct... I stole it directly from his source control
